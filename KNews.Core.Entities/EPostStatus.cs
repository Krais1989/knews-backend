namespace KNews.Core.Entities
{
    /// <summary>
    /// Статус поста
    /// </summary>
    public enum EPostStatus : int
    {
        Created = 0,    // В процессе создания
        Check,          // Проверяется модерацией
        Approved,       // Подтверждено модератором
        Forbiden,       // Отклонено модератором
        Deleted         // 
    }
}
