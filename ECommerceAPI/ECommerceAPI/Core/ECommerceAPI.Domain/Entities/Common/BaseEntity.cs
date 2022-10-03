namespace ECommerceAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // To override this. Because I do not want to map to the database 
        virtual public DateTime UpdatedDate { get; set; }

    }
}
