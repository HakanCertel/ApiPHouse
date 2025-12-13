using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.CategoriesE
{
    public class SubCategory_1:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public Guid ParentId { get; set; }  

        public MainCategory Parent { get; set; }
        public ICollection<SubCategory_2> SubCategory_2List { get; set; }
    }
}
