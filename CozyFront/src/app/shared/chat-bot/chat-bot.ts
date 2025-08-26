import { Component, ElementRef, Renderer2, OnInit, OnDestroy } from '@angular/core';

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

  constructor(private el: ElementRef, private renderer: Renderer2) {}

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
}
