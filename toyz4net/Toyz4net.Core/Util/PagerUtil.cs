using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Toyz4net.Core.Util
{
     public   abstract  class PagerUtil
    {
    }

     public class PagerObject {

         public int page { get; private set; }
         public int size { get; private set; }
         public int startIndex { get; private set; }
         public int endIndex { get; private set; }

         public PagerObject(int page,int size) {
             this.page = page;
             this.size = size;
             this.startIndex = (page-1) * size;
             this.endIndex = startIndex + size;
         }
     }

     public class PageList<T> : LinkedList<T>,IList {


         public PageList(IList<T> list, PagerObject pager) {
 
             this.total = list.Count;
             for (int i = pager.startIndex; (i < this.total)&&(i<pager.endIndex); i++) {
                 this.AddLast(list.ElementAt<T>(i));
             }

         }

         public PageList(IList<T> list)
         {

             this.total = list.Count;
             for (int i = 0; i<this.total; i++)
             {
                 this.AddLast(list.ElementAt<T>(i));
             }

         }

         public PageList(IList<T> list,int total)
         {

             this.total = total;
             int pageCount = list.Count;
             for (int i = 0; i < pageCount; i++)
             {
                 this.AddLast(list.ElementAt<T>(i));
             }

         }

         public int total { get;private set; }



         #region IList 成员

         public int Add(object value)
         {
             throw new NotImplementedException();
         }

         public bool Contains(object value)
         {
             throw new NotImplementedException();
         }

         public int IndexOf(object value)
         {
             throw new NotImplementedException();
         }

         public void Insert(int index, object value)
         {
             throw new NotImplementedException();
         }

         public bool IsFixedSize
         {
             get { throw new NotImplementedException(); }
         }

         public bool IsReadOnly
         {
             get { throw new NotImplementedException(); }
         }

         public void Remove(object value)
         {
             throw new NotImplementedException();
         }

         public void RemoveAt(int index)
         {
             throw new NotImplementedException();
         }

         public object this[int index]
         {
             get
             {
                 throw new NotImplementedException();
             }
             set
             {
                 throw new NotImplementedException();
             }
         }

         #endregion
     }

}
