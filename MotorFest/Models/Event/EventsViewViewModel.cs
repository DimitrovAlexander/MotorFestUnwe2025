namespace MotorFest.Models.Event
{
    public class EventsViewViewModel
    {
        public int EventId { get; set; } // Идентификатор на събитието
        public string OrganizerId { get; set; } // Идентификатор на организатора
        public string EventName { get; set; } // Име на събитието
        public string LocationName { get; set; } // Име на локацията
        public DateTime EventDate { get; set; } // Дата на събитието
        public decimal EntranceFee { get; set; } // Цена за вход
        public int RegisteredParticipantsCount { get; set; } // Брой регистрирани участници
        public decimal ExpectedRevenue { get; set; } // Очакван приход
        public int AllowedEngineTypesCount { get; set; } // Брой допустими типове двигатели
        public int AllowedVehicleCategoriesCount { get; set; } // Брой допустими категории превозни средства
    }
}
