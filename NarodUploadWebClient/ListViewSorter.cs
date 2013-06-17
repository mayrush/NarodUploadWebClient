namespace NarodUploadWebClient
{
    using System.Collections;
    using System.Windows.Forms;

    ///<summary>Сортировка колонок в ListView</summary>
    public class ListViewSorter : IComparer
    {
        public int Compare(object o1, object o2)
        {
            if (!(o1 is ListViewItem))
            {
                return 0;
            }
            if (!(o2 is ListViewItem))
            {
                return 0;
            }

            var item = (ListViewItem)o2;
            var item2 = (ListViewItem)o1;

            if (((item.ImageIndex == 7) || (item2.ImageIndex == 7)) || ((item.ImageIndex == 4) || (item2.ImageIndex == 4)))
            {
                return 0;
            }

            string text = item.SubItems[ByColumn].Text;
            string strB = item2.SubItems[ByColumn].Text;
            int num = item.ListView.Sorting == SortOrder.Ascending ? string.Compare(text, strB) : string.Compare(strB, text);
            LastSort = ByColumn;
            return num;
        }

        ///<summary>Возвращает и устанавливает колонку</summary>
        public int ByColumn { get; set; }

        ///<summary>Последняя сортировка</summary>
        public int LastSort { get; set; }
    }
}