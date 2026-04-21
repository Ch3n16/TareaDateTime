using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        CultureInfo culturaES = new CultureInfo("es-ES");

        // --- Nombre completo ---
        Console.Write("Introduce tu nombre completo: ");
        string nombreCompleto = Console.ReadLine() ?? "";

        int espacioIndex = nombreCompleto.IndexOf(' ');
        string primerNombre = espacioIndex >= 0
            ? nombreCompleto.Substring(0, espacioIndex)
            : nombreCompleto;

        string apellidos = espacioIndex >= 0
            ? nombreCompleto.Substring(espacioIndex + 1)
            : "";

        // --- Fecha de nacimiento con validación ---
        DateTime fechaNacimiento;
        while (true)
        {
            Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
            string? entrada = Console.ReadLine();

            if (DateTime.TryParseExact(entrada, "dd/MM/yyyy", culturaES, DateTimeStyles.None, out fechaNacimiento))
            {
                if (fechaNacimiento > DateTime.Today)
                {
                    Console.WriteLine("La fecha de nacimiento no puede ser en el futuro. Inténtalo de nuevo.");
                    continue;
                }
                break;
            }
            Console.WriteLine("Formato no válido. Usa dd/MM/yyyy (ejemplo: 15/06/1990).");
        }

        DateTime hoy = DateTime.Today;

        // --- Edad exacta ---
        int edad = hoy.Year - fechaNacimiento.Year;
        if (hoy < fechaNacimiento.AddYears(edad))
            edad--;

        // --- Fecha en formato largo ---
        string fechaLarga = fechaNacimiento.ToString("dddd, d 'de' MMMM 'de' yyyy", culturaES);

        // --- Signo del zodiaco ---
        string signo = ObtenerSigno(fechaNacimiento.Month, fechaNacimiento.Day);

        // --- Días hasta el próximo cumpleaños ---
        DateTime proximoCumple = new DateTime(hoy.Year, fechaNacimiento.Month, fechaNacimiento.Day);
        if (proximoCumple < hoy)
            proximoCumple = proximoCumple.AddYears(1);

        int diasHastaCumple = (proximoCumple - hoy).Days;

        // --- Informe ---
        Console.WriteLine();
        Console.WriteLine($"Hola, {primerNombre}!");
        Console.WriteLine($"Tienes {edad} años.");
        Console.WriteLine($"Tu cumpleaños es el {fechaLarga}.");
        Console.WriteLine($"Tu signo del zodiaco es {signo}.");

        if (diasHastaCumple == 0)
            Console.WriteLine("¡Hoy es tu cumpleaños! ¡Felicidades!");
        else
            Console.WriteLine($"Faltan {diasHastaCumple} días para tu próximo cumpleaños.");

        if (apellidos.Length > 0)
            Console.WriteLine($"Apellidos: {apellidos}");

        Console.WriteLine();
        Console.Write("Pulsa una tecla para salir...");
        Console.ReadKey();
    }

    static string ObtenerSigno(int mes, int dia)
    {
        return (mes, dia) switch
        {
            (1, >= 20) or (2, <= 18) => "Acuario",
            (2, >= 19) or (3, <= 20) => "Piscis",
            (3, >= 21) or (4, <= 19) => "Aries",
            (4, >= 20) or (5, <= 20) => "Tauro",
            (5, >= 21) or (6, <= 20) => "Géminis",
            (6, >= 21) or (7, <= 22) => "Cáncer",
            (7, >= 23) or (8, <= 22) => "Leo",
            (8, >= 23) or (9, <= 22) => "Virgo",
            (9, >= 23) or (10, <= 22) => "Libra",
            (10, >= 23) or (11, <= 21) => "Escorpio",
            (11, >= 22) or (12, <= 21) => "Sagitario",
            _ => "Capricornio"
        };
    }
}
