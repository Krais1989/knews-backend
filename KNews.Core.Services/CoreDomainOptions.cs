using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services
{
    public class CoreDomainOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public long DefaultCommunityID { get; set; }

        /// <summary>
        /// Период времени с момента создания поста в который можно его редактировать 
        /// </summary>
        public TimeSpan UpdateAvailablePeriod { get; set; }
    }
}
