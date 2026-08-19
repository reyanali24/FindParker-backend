namespace ClassLibraryModels
{
    public class PublicQrCodeModel
    {
        public long QrId { get; set; }

        public string? QrCodeValue { get; set; }

        public long VehicleId { get; set; }

        public bool IsAssigned { get; set; }

        public string? Status { get; set; }

        public string? VehicleName { get; set; }

        public string? PlateNumber { get; set; }

        public string? Color { get; set; }

        public string? VehicleType { get; set; }
    }
}