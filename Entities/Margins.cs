namespace HtmlToPdfSharp.Entities
{
    /// <summary>
    /// Size of the margins in millimeters
    /// </summary>
    public class Margins
    {
        /// <summary>
        /// Size of the top margin
        /// </summary>
        [Settings("margin.top", SpecifyUnitsType = true)]
        public float? Top { get; set; }

        /// <summary>
        /// Size of the bottom margin
        /// </summary>
        [Settings("margin.bottom", SpecifyUnitsType = true)]
        public float? Bottom { get; set; }

        /// <summary>
        /// Size of the left margin
        /// </summary>
        [Settings("margin.left", SpecifyUnitsType = true)]
        public float? Left { get; set; }

        /// <summary>
        /// Size of the right margin
        /// </summary>
        [Settings("margin.right", SpecifyUnitsType = true)]
        public float? Right { get; set; }

        public Margins()
        {
        }

        public Margins(float? top, float? bottom, float? left, float? right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }
}