﻿using Microsoft.WindowsAzure;
using System;

namespace WamlDemos
{
    public partial class TraceWindow
    {
        private static readonly Lazy<TraceWindow> TraceWindowLazy = new Lazy<TraceWindow>(() => new TraceWindow());

        private TraceWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) => StartTrace();
        }

        public static TraceWindow Instance
        {
            get { return TraceWindowLazy.Value; }
        }

        private void StartTrace()
        {
            Action<string> traceAction = trace => Dispatcher.Invoke(() => Trace.Text = trace + Environment.NewLine + Trace.Text);
            CloudContext.Configuration.Tracing.AddTracingInterceptor(new WpfAppTracingInterceptor(traceAction));
        }
    }
}
