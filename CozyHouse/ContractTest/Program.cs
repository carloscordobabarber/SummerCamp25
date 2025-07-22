Contract contract = new Contract(
            contractId: 123,
            clientDni: "12345678X",
            clientName: "Juan Pérez",
            clientBankAccount: "ES7620770024003102575766",
            payment: 1200,
            startDate: new DateTime(2025, 8, 1),
            endDate: new DateTime(2026, 7, 31),
            apartmentAddress: "Calle Falsa 123, Madrid");

Console.WriteLine("Información del contrato:");
Console.WriteLine($"ID Contrato: {contract.ContractId}");
Console.WriteLine($"DNI Cliente: {contract.ClientDni}");
Console.WriteLine($"Nombre Cliente: {contract.ClientName}");
Console.WriteLine($"Cuenta Bancaria: {contract.ClientBankAccount}");
Console.WriteLine($"Pago Mensual: {contract.Payment} €");
Console.WriteLine($"Fecha Inicio: {contract.StartDate:dd/MM/yyyy}");
Console.WriteLine($"Fecha Fin: {contract.EndDate:dd/MM/yyyy}");
Console.WriteLine($"Dirección Apartamento: {contract.ApartmentAddress}");