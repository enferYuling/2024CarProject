using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Method
{
    public class CustomTreeView : TreeView
    {
        public CustomTreeView()
        {
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
        }
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                // 判断节点是否展开
                bool isExpanded = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
                // 获取节点绘制区域
                Rectangle nodeRect = e.Bounds;
                // 绘制三角形
                Point[] pts = new Point[3];
                if (isExpanded)
                {
                    pts[0] = new Point(nodeRect.Left, nodeRect.Top);
                    pts[1] = new Point(nodeRect.Left + 8, nodeRect.Top);
                    pts[2] = new Point(nodeRect.Left + 4, nodeRect.Top + 8);
                }
                else
                {
                    pts[0] = new Point(nodeRect.Left, nodeRect.Top + 4);
                    pts[1] = new Point(nodeRect.Left + 8, nodeRect.Top);
                    pts[2] = new Point(nodeRect.Left + 8, nodeRect.Top + 8);
                }
                e.Graphics.FillPolygon(Brushes.Black, pts);
                // 绘制节点文本
                e.Graphics.DrawString(e.Node.Text, this.Font, Brushes.Black,
                    nodeRect.Left + 12, nodeRect.Top);
            }
            else
            {
                // 非根节点使用默认绘制方式
                base.OnDrawNode(e);
            }
        }
    }
}
