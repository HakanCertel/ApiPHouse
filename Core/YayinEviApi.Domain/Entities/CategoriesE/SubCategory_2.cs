using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.CategoriesE
{
    public class SubCategory_2:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public Guid ParentId { get; set; }
        public SubCategory_1 Parent { get; set; }
        public ICollection<SubCategory_3> SubCategory_3List { get; set; }

    }
}
