using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Allowerd.Libiray.NPC
{
    public class APlayerAI : MonoBehaviour
    {
        #region [Defines]
        /// <summary>
        /// Солько раз в секунду производится расчет
        /// </summary>
        public float RateUpdatePerSecond { get { return this.m_timerateupdatepersecond; } set { this.m_timerateupdatepersecond = 1f / value; } }
        /// <summary>
        /// С какой скоростью бежать в секунду.
        /// </summary>
        public float RateMoveingSpeed { get; set; } = 5f;
        /// <summary>
        /// Как высоко или низко можно двигатся по оси Y
        /// </summary>
        public float RateMaxHeight { get; set; } = 1f;

        private float m_tickrateupdate = 0f;
        private float m_timerateupdatepersecond = 0.1f;
        private float m_timeratemoveingspeed => this.RateMoveingSpeed * this.m_timerateupdatepersecond;
        #endregion

        #region [Method] Awake
        private void Awake()
        {

        }
        #endregion

        #region [Method] Update
        private void Update()
        {
            this.m_tickrateupdate += Time.deltaTime;
            if (this.m_tickrateupdate >= this.m_timeratemoveingspeed)
            {
                this.m_tickrateupdate = 0f;
                this.CustomUpdate();
            }
        }
        #endregion

        #region [Method] CustomUpdate
        private void CustomUpdate()
        {

        }
        #endregion
    }
}
