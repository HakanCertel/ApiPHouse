using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories
{
    public class GetNewCodeRepository<T> : GeneralRepository<T>, IGetNewCode<T> where T : BaseEntity
    {
        public GetNewCodeRepository(YayinEviApiDbContext context) : base(context)
        {
        }

        public async Task<string> GetNewCodeAsync(string serie, Expression<Func<T, string>> filter)
        {
            string Kod()//bu metod işlem yaptığımız tabloda veri yoksa bu metod çalışarak ilk kodu verecektir.....
            {
                string kod = null;
                var kodDizi = serie.Split(' ');//bu ifade,string ifade içinde, diziyi herboşluk sonrası kaç adet ifade olamasına göre ayıracak

                for (int i = 0; i < kodDizi.Length; i++)
                {
                    kod += kodDizi[i];

                    if (i + 1 < kodDizi.Length - 1)
                        kod += " ";
                }

                return kod += "000000001";
            }

            string YeniKodVer(string kod)
            {
                var sayisalDegerler = "";

                foreach (var karakter in kod)
                {
                    if (char.IsDigit(karakter))
                        sayisalDegerler += karakter;
                    else
                        sayisalDegerler = "";
                }

                var artisSonrasiDeger = (int.Parse(sayisalDegerler) + 1).ToString();
                var fark = kod.Length - artisSonrasiDeger.Length;
                if (fark < 0)
                    fark = 0;

                var yeniDeger = kod.Substring(0, fark);
                yeniDeger += artisSonrasiDeger;
                return yeniDeger;

            }

            var maxKod = Table.Where(x => x.Code.Contains(serie)).MaxAsync(x=>x.Code).Result?.ToString();
            return maxKod == null ? Kod() : YeniKodVer(maxKod);
            //throw new NotImplementedException();
        }
    }
}
