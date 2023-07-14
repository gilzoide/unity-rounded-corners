using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.RoundedCorners.Samples.RoundedGraphics
{
    [RequireComponent(typeof(RectTransform)), ExecuteAlways]
    public class FlexibleCellSizeGridLayoutGroup : GridLayoutGroup
    {
        public void RefreshCellSize()
        {
            int childrenMainAxisCount = constraintCount;
            int childrenSecondaryAxisCount = Mathf.CeilToInt((float) rectChildren.Count / constraintCount);
            Vector2 size = rectTransform.rect.size - new Vector2(padding.horizontal, padding.vertical);
            switch (constraint)
            {
                case Constraint.Flexible:
                    return;

                case Constraint.FixedColumnCount:
                    cellSize = new Vector2(
                        (size.x - (spacing.x * (childrenSecondaryAxisCount - 1))) / childrenMainAxisCount,
                        (size.y - (spacing.y * (childrenMainAxisCount - 1))) / childrenSecondaryAxisCount
                    );
                    break;

                case Constraint.FixedRowCount:
                    cellSize = new Vector2(
                        (size.x - (spacing.x * (childrenMainAxisCount - 1))) / childrenSecondaryAxisCount,
                        (size.y - (spacing.y * (childrenSecondaryAxisCount - 1))) / childrenMainAxisCount
                    );
                    break;
            }
        }

        public override void SetLayoutHorizontal()
        {
            RefreshCellSize();
            base.SetLayoutHorizontal();
        }

    #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            RefreshCellSize();
        }
    #endif
    }
}

