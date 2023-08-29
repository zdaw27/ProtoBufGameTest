using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program {

    public static float deltaTime = 0f;

    static void Main(string[] args) {
        ProtocolManager.GetInstance().Register();
        ProtocolDispatcher.GetInstance().Register();
        SessionServer.GetInstance().Start();
        BattleServer.GetInstance().Start();

        var sw = new Stopwatch();
        
        int frameLimit = 1000 / 30; // 1000ms / 30fps
        while (true) {
            sw.Restart();
            
            foreach (var zoneController in BattleServer.GetInstance().GetZoneControllerPool()) {
                Task.Run(() => {
                    zoneController.Update();
                });
            }
            

            sw.Stop();
            var elapsed = (int)sw.ElapsedMilliseconds;
            
            var sleepDuration = frameLimit - elapsed;
            deltaTime = (float)sleepDuration * 0.001f;
            if (sleepDuration > 0) {
                Thread.Sleep(sleepDuration);
            }
        }
    }
}