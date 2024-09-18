using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarProject.Method
{


    public class ArtificialHorizon : Control
    {
        private float roll = 0; // 滚转角度
        private float pitch = 0; // 俯仰角度

        public ArtificialHorizon()
        {
            this.DoubleBuffered = true; // 减少闪烁
            this.Size = new Size(200, 200); // 控件大小
        }

        public float Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                Invalidate(); // 触发重绘
            }
        }

        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                Invalidate(); // 触发重绘
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.Black); // 使用黑色作为背景

            // 绘制地平线
            DrawSkyAndGround(g);
            DrawAirplane(g);
        }
        private void DrawSkyAndGround(Graphics g)
        {
            int centerX = this.Width / 2;
            int centerY = this.Height / 2;
            int radius = Math.Min(centerX, centerY);

            // 绘制天空和地面
            using (Brush skyBrush = new SolidBrush(Color.LightBlue))
            using (Brush groundBrush = new SolidBrush(Color.Gray))
            {
                g.FillPie(skyBrush, centerX - radius / 2, centerY - radius / 2, radius, radius, 90, 180);
                g.FillPie(groundBrush, centerX - radius / 2, centerY + radius / 2 - 180, radius, radius, 270, 180);
            }

            // 绘制地平线中心线
            g.DrawLine(Pens.White, centerX - radius / 2, centerY, centerX + radius / 2, centerY);
        }

        private void DrawAirplane(Graphics g)
        {
            int airplaneSize = 20;
            int airplaneX = (this.Width - airplaneSize) / 2;
            int airplaneY = this.Height / 2 - (int)(pitch / 90.0 * (this.Height / 4));

            // 根据滚转角度旋转飞机
            g.RotateTransform(roll);
            g.TranslateTransform(airplaneX, airplaneY);

            // 绘制飞机
            using (Brush airplaneBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(airplaneBrush, -airplaneSize / 2, -airplaneSize / 2, airplaneSize, airplaneSize);
            }

            // 重置图形转换
            g.ResetTransform();
        }
    

}
}
