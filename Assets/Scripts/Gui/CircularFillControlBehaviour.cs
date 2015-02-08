using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public class CircularFillControlBehaviour : MonoBehaviour, ICircularFillControlBehaviour
    {
        #region Unity Properties
        public Image CurrentValueFillPanel;

        public Image BackgroundFillPanel;

        public Color InitialFillColor;

        public Color InitialBackgroundColor;
        #endregion

        #region Properties
        public float Value
        {
            get { return CurrentValueFillPanel.fillAmount; }
            set { CurrentValueFillPanel.fillAmount = value; }
        }

        public Color FillColor
        {
            get { return CurrentValueFillPanel.color; }
            set { CurrentValueFillPanel.color = value; }
        }

        public Color BackgroundColor
        {
            get { return BackgroundFillPanel.color; }
            set { BackgroundFillPanel.color = value; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            Value = 1f;
            FillColor = InitialFillColor;
            BackgroundColor = InitialBackgroundColor;
        }
        #endregion
    }

    [ContractClass(typeof(ICircularFillControlBehaviourContract))]
    public interface ICircularFillControlBehaviour
    {
        #region Properties
        float Value { get; set; }

        Color FillColor { get; set; }

        Color BackgroundColor { get; set; }
        #endregion
    }

    [ContractClassFor(typeof(ICircularFillControlBehaviour))]
    public abstract class ICircularFillControlBehaviourContract : ICircularFillControlBehaviour
    {
        #region Properties
        public float Value
        {
            get
            {
                Contract.Ensures(Contract.Result<float>() >= 0f && Contract.Result<float>() <= 1f);

                return default(float);
            }

            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0f && value <= 1f);
            }
        }

        public abstract Color FillColor { get; set; }

        public abstract Color BackgroundColor { get; set; }
        #endregion
    }
}