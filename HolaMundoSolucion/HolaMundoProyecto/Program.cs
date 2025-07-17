
String entrada;

do {
    Console.WriteLine("Elija un numero del 1 al 7, introduzca 0 para salir");
     entrada = Console.ReadLine();
    
    switch (entrada) {
        case "1":
            Console.WriteLine("Lunes");
            break;
        case "2":
            Console.WriteLine("Martes");
            break;
        case "3":
            Console.WriteLine("Miercoles");
            break;
        case "4":
            Console.WriteLine("Jueves");
            break;
        case "5":
            Console.WriteLine("Viernes");
            break;
        case "6":
            Console.WriteLine("Sabado");
            break;
        case "7":
            Console.WriteLine("Domingo");
            break;
        default:
            Console.WriteLine("Elija un numero valido");
            break;

    }
} while (entrada!="0");