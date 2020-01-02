namespace KNews.Core.Entities
{
    /// <summary>
    /// Статус поста
    /// </summary>
    public enum EPostStatus : int
    {
        Init = 0,       // В процессе создания
        Check,      // Проверяется модерацией
        Approved,   // Подтверждено
        Deleted     // 
    }
}
