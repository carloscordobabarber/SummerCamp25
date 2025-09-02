import { Component, ElementRef, Renderer2, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat-bot',
  standalone: false,
  templateUrl: './chat-bot.html',
  styleUrl: './chat-bot.css'
})
export class ChatBot implements OnInit, OnDestroy {
  isOpen = false;
  elevated = false;
  private observer?: IntersectionObserver;

  messages: { text: string, from: 'user' | 'bot' }[] = [
    { text: 'Soy su agente inmobiliario virtual, estoy listo para ayudarle', from: 'bot' }
  ];
  userInput: string = '';

  constructor(private el: ElementRef, private renderer: Renderer2, private router: Router) {}

  ngOnInit() {
    // Espera a que el DOM esté listo y busca el footer
    setTimeout(() => {
      const footer = document.querySelector('footer, .simple-footer');
      if (footer) {
        this.observer = new IntersectionObserver((entries) => {
          entries.forEach(entry => {
            if (entry.isIntersecting) {
              this.elevated = true;
            } else {
              this.elevated = false;
            }
          });
        }, {
          root: null,
          threshold: 0.01
        });
        this.observer.observe(footer);
      }
    }, 500);
  }

  ngOnDestroy() {
    if (this.observer) {
      this.observer.disconnect();
    }
  }

  openChat() {
    this.isOpen = true;
  }

  closeChat() {
    this.isOpen = false;
  }

  sendMessage() {
    const text = this.userInput.trim();
    if (text) {
      this.messages.push({ text, from: 'user' });
      this.userInput = '';
      this.handleBotResponse(text);
    }
  }

  handleBotResponse(userText: string) {
    const lowerText = userText.toLowerCase();
    // Árbol de conversación ampliado
    if (/(ayuda|help|cómo usar|como usar|qué puedo hacer|que puedo hacer)/.test(lowerText)) {
      this.messages.push({
        text: 'Puede escribirme lo que necesita usando palabras clave como "alquilar", "filtros", "registro", "contacto" o "servicios personales". Yo le guiaré o le redirigiré según su consulta. Si no sabe por dónde empezar, simplemente escriba su duda y le ayudaré a encontrar la opción adecuada.',
        from: 'bot'
      });
      return;
    }
    if (/(alquilar|piso|apartamento|buscar)/.test(lowerText)) {
      this.messages.push({
        text: '¡Perfecto! Te redirijo a la página de inicio para que veas los pisos disponibles.',
        from: 'bot'
      });
      this.simulateRedirect('inicio');
      return;
    }
    if (/(filtro|filtrar|buscar por)/.test(lowerText)) {
      this.messages.push({
        text: 'La barra de filtros te permite buscar pisos según tus preferencias. Puedes filtrar por precio, ubicación y número de habitaciones. Además, puedes combinar varios filtros para afinar tu búsqueda y encontrar el piso ideal de forma rápida y sencilla.',
        from: 'bot'
      });
      return;
    }
    if (/(registrarse|registro|crear cuenta|alta)/.test(lowerText)) {
      this.messages.push({
        text: 'Para acceder a las funciones avanzadas de la aplicación, como gestionar tus alquileres, registrar incidencias o ver tus contratos, es necesario registrarse. El proceso de registro es sencillo y rápido. ¿Quieres que te redirija a la página de registro?',
        from: 'bot'
      });
      // Ofrece redirección si el usuario responde afirmativamente en el siguiente mensaje
      return;
    }
    if (/(sí|si|quiero registrarme|redirige|adelante)/.test(lowerText) && this.messages.length > 1 && this.messages[this.messages.length-2].text.includes('¿Quieres que te redirija a la página de registro?')) {
      this.messages.push({
        text: 'Te estoy redirigiendo a la página de registro.',
        from: 'bot'
      });
      this.simulateRedirect('registro');
      return;
    }
    if (/(contacto|hablar|soporte)/.test(lowerText)) {
      this.messages.push({
        text: 'Puedes contactar con nuestro equipo desde la página de contactos. Allí encontrarás diferentes formas de ponerte en contacto con nosotros para resolver cualquier duda o incidencia.',
        from: 'bot'
      });
      this.simulateRedirect('contactos');
      return;
    }
    if (/(servicio personal|servicios personales|perfil|mis datos|mis pisos|contrato|incidencia|incidencias|alquilados|gestión personal)/.test(lowerText)) {
      this.messages.push({
        text: 'En tu perfil personal puedes acceder a varias funciones avanzadas: ver los datos de los pisos que tienes alquilados, consultar y descargar tus contratos, y utilizar el sistema de incidencias para registrar y revisar cualquier problema relacionado con tu alquiler. Todo está organizado en categorías para que puedas gestionar tu experiencia de forma sencilla y segura.',
        from: 'bot'
      });
      return;
    }
    // Respuesta por defecto
    this.messages.push({
      text: 'No he entendido su solicitud. ¿Puede darme más detalles o usar palabras como "alquilar", "filtros", "registro", "contacto" o "servicios personales"?',
      from: 'bot'
    });
  }

  simulateRedirect(destino: string) {
    // Enrutamiento real según destino
    switch (destino) {
      case 'inicio':
        this.router.navigate(['']);
        break;
      case 'registro':
        this.router.navigate(['/register']);
        break;
      case 'contactos':
        this.router.navigate(['/contact']);
        break;
      default:
        console.log('Redirigiendo a:', destino);
    }
  }
}
