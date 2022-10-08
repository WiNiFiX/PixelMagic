﻿//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ShadowMagic.GUI;
using ShadowMagic.Helpers;
using System.Media;
using System.Runtime.InteropServices;

// ReSharper disable FunctionNeverReturns
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace ShadowMagic.Rotation
{
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public abstract class CombatRoutine
    {
        public enum RotationState
        {
            Stopped = 0,
            Running = 1
        }

        public enum RotationType
        {
            SingleTarget = 0,
            SingleTargetCleave = 1,     // Some classes like Warriors need support for this
            AOE = 2
        }

        private volatile RotationType _rotationType = RotationType.SingleTarget;

        private Thread characterInfo;
        public CombatRoutine combatRoutine;
        private Thread mainThread;

        private frmMain parent;

        private readonly ManualResetEvent pause = new ManualResetEvent(false);

        private int PulseFrequency = 100;

        private readonly Random random;

        public CombatRoutine()
        {
            random = new Random(DateTime.Now.Second);
        }

        public RotationState State { get; private set; } = RotationState.Stopped;

        public RotationType Type => _rotationType;

        public abstract string Name { get; }
        public abstract string Class { get; }

        public void CharacterInfoThread()
        {
            while (true)
            {
                pause.WaitOne();

                Threads.UpdateTextBox(parent.txtPlayerHealth, WoW.HealthPercent.ToString());
                Threads.UpdateTextBox(parent.txtPlayerPower, WoW.Power.ToString());
                Threads.UpdateTextBox(parent.txtTargetHealth, WoW.TargetHealthPercent.ToString());
                Threads.UpdateTextBox(parent.txtTargetCasting, WoW.TargetIsCasting.ToString());
                Threads.UpdateTextBox(parent.txtRange, WoW.CountEnemyNPCsInRange.ToString());

                Thread.Sleep(500);
            }
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private void MainThreadTick()
        {
            try
            {
                while (true)
                {
                    var key = GetAsyncKeyState(Keys.LShiftKey);
                    if ((key & 0x8000) != 0)
                    {
                        // Pause rotation when left shift is down
                    }
                    else
                    {
                        pause.WaitOne();
                                                
                        Pulse();
                    }
                    
                    Thread.Sleep(PulseFrequency + random.Next(50));
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
            Thread.Sleep(random.Next(50)); // Make the bot more human-like add some randomness in
        }

        public void Load(frmMain parent)
        {
            this.parent = parent;

            PulseFrequency = int.Parse(ConfigFile.Pulse.ToString());
            Log.Write("Using Pulse Frequency (ms) = " + PulseFrequency);

            characterInfo = new Thread(CharacterInfoThread) {IsBackground = true};
            characterInfo.Start();

            mainThread = new Thread(MainThreadTick) {IsBackground = true};
            mainThread.Start();

            combatRoutine = this;
            
            Initialize();
        }

        public string FileName = "";

        internal void Dispose()
        {
            Log.Write("Stopping Character Info Thread...");
            Log.Write("Stopping Pulse() timer...");

            Pause();

            Thread.Sleep(100); // Wait for it to close entirely so that all bitmap reading is done
        }

        internal void ForcePulseUpdate()
        {
            PulseFrequency = int.Parse(ConfigFile.Pulse.ToString());
            Log.Write("Using Pulse Frequency (ms) = " + PulseFrequency);
        }

        public void Start()
        {
            try
            {
                if (State == RotationState.Stopped)
                {
                    Log.Write("Starting bot...", Color.Green);
                    
                    if (WoW.Process == null)
                    {
                        Log.Write("World of warcraft is not detected / running, please login before attempting to restart the bot", Color.Red);
                        return;
                    }

                    pause.Set();

                    State = RotationState.Running;                    
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error Starting Combat Routine", Color.Red);
                Log.Write(ex.Message, Color.Red);
            }
        }

        public void Pause()
        {
            try
            {
                if (State == RotationState.Running)
                {
                    Log.Write("Stopping bot.", Color.Black);

                    Stop();

                    pause.Reset();

                    State = RotationState.Stopped;

                    Log.Write("Combat routine has been stopped sucessfully.", Color.IndianRed);
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error Stopping Combat Routine", Color.Red);
                Log.Write(ex.Message, Color.Red);
            }
        }

        public void ChangeType(RotationType rotationType)
        {
            if (_rotationType == rotationType) return;

            _rotationType = rotationType;

            Log.Write("Rotation type: " + rotationType);

            WoW.Speak(rotationType.ToString());

            if (ConfigFile.PlayErrorSounds)
            {
                SystemSounds.Beep.Play();
            }
        }

        private bool useCooldowns;

        public bool UseCooldowns
        {
            get
            {
                return useCooldowns;
            }
            set
            {
                useCooldowns = value;

                Log.Write("UseCooldowns = " + value);
            }
        }

        public abstract void Initialize();
        public abstract void Stop();
        public abstract void Pulse();
        public abstract Form SettingsForm { get; set; }
    }
}