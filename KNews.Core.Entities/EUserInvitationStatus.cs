namespace KNews.Core.Entities
{
    public enum EUserInvitationStatus
    {
        Recieved,   // Приглашение получено
        Accepted,   // Принял приглашение
        Declined,   // Отверг приглашение
        Ignored     // Проигнорировал приглашение в течение времени ожидания
    }
}
