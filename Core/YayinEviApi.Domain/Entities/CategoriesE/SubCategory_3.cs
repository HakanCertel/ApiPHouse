using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.CategoriesE
{
    public class SubCategory_3:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Guid ParentId { get; set; }
        public SubCategory_2 Parent { get; set; }
        public ICollection<SubCategory_4> SubCategory_4List { get; set; }

    }
}
