using YayinEviApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YayinEviApi.Infrastructure.Operations
{
    public static class ConvertEntity
    {
        public static TTarget EntityCovert<TTarget>(this IBaseEntity kaynak)
        {
            if (kaynak == null) return default(TTarget);
            
            var hedef = Activator.CreateInstance<TTarget>();
            
            var kaynakProp = kaynak.GetType().GetProperties();

            var hedefProp = typeof(TTarget).GetProperties();
            foreach (var kp in kaynakProp)
            {
                if (kp.PropertyType.Namespace == "System.Collections.Generic") continue;
               
                if (typeof(TTarget).GetProperty(kp.Name) == null) continue;
                
                var value = kp.GetValue(kaynak);
                var hp = hedefProp.FirstOrDefault(x => x.Name == kp.Name);
                
                if (hp != null)
                {
                    Type hpType=hp.PropertyType;
                    Type kpType = kp.PropertyType;
                    //bu string değeri  guid e çevirmek için yazıldı
                    Type enumType=hpType.IsEnum?hpType:Nullable.GetUnderlyingType(hpType);
                    if (hp.PropertyType.IsEnum || kp.PropertyType.IsEnum)
                    {
                        var sourceValue = kp.GetValue(kaynak);
                        string enumString = sourceValue?.ToString();

                        if (!string.IsNullOrEmpty(enumString)&&hp.PropertyType.IsEnum)
                        {
                            object enumValue = EnumFunction.GetEnumValueFromDescription(enumType, enumString);
                            //object enumValue = Enum.Parse(enumType, "product", true); // true: Case-insensitive (Büyük/küçük harf duyarsız)
                            hp.SetValue(hedef, enumValue);
                            continue;
                        }

                         hp.SetValue(hedef, enumString);

                        continue;

                    }
                   
                    //if (enumType != null && enumType.IsEnum)
                    //{
                    //    object sourceValue = kp.GetValue(kaynak);
                    //    string enumString = sourceValue?.ToString();
                        
                    //    if (!string.IsNullOrEmpty(enumString))
                    //    {
                    //        object enumValue = EnumFunction.GetEnumValueFromDescription(enumType,enumString);
                    //        //object enumValue = Enum.Parse(enumType, "product", true); // true: Case-insensitive (Büyük/küçük harf duyarsız)
                    //        hp.SetValue(hedef, enumValue);
                    //        continue;
                    //    }
                    //}
                    else if (enumType?.Name == "Guid"&&value!=null)
                    {
                        object sourceValue =value!=null||value!=""? Guid.Parse(value.ToString()):null;
                        hp.SetValue(hedef, sourceValue);
                        continue;
                    }
                    hp.SetValue(hedef, ReferenceEquals(value, "") ? null : value);
                }
            }
            return hedef;
        }

        public static IEnumerable<TTarget> EntityListConvert<TTarget>(this IEnumerable<IBaseEntity> source)
        {
            return source?.Select(x => x.EntityCovert<TTarget>()).ToList();
        }
        
    }
}
