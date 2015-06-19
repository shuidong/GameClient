using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI
{
    public interface MonoInterface
    {
        void Awake(params object[] data);
        void OnEnable(params object[] data);
        void Start(params object[] data);
        void Update(params object[] data);
        void FixedUpdate(params object[] data);
        void OnDestory(params object[] data);
        void OnDisable(params object[] data);
        void OnGUI(params object[] data);
    }
}
