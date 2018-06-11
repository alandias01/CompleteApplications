using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Diagnostics;

using Maple.Dtc;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using System.Threading;
using Maple.Global1.G1BizObjects;

namespace Maple.Dtc.PositionClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public class DtcChannel : IDtcFeedCallback
    {
        private static readonly DtcChannel instance = new DtcChannel();

        private InstanceContext instanceContext;        
        public DuplexChannelFactory<IDtcFeed> factory;
        public IDtcFeed channel;

        public static DtcUserObject User;
        
        private static object locker = new object();


        #region Events

        public event IncomingDtcMessageEventHandler handleDtcMessageReceived;
        public event IncomingG1MessageEventHandler handleG1MessageReceived;

        public event UserJoinEventHandler UserJoined;
        public event UserLeaveEventHandler UserLeaves;

        public event CusipSelectEventHandler handleCusipSelected;

        public event StockInfoUpdateEventHandler handleStockInfoUpdated;
        public event StockPriceUpdateEventHandler handleStockPriceUpdated;

        public event PositionUpdateEventHandler handlePositionUpdated;


        protected virtual void OnPositionUpdated(PositionUpdateEventArgs e)
        {
            if (handlePositionUpdated != null)
            {
                handlePositionUpdated(this, e);
            }
        }

        protected virtual void OnDtcMessageReceived(IncomingDtcMessageEventArgs e)
        {
            if (handleDtcMessageReceived != null)
            {
                handleDtcMessageReceived(this, e);
            }
        }

        protected virtual void OnG1MessageReceived(IncomingG1MessageEventArgs e)
        {
            if (handleG1MessageReceived != null)
            {
                handleG1MessageReceived(this, e);
            }
        }
        
        protected virtual void OnCusipSelected(CusipSelectEventArgs e)
        {
            if (handleCusipSelected != null)
            {
                handleCusipSelected(this, e);
            }
        }

        protected virtual void OnStockInfoUpdated(StockInfoUpdateEventArgs e)
        {
            if (handleStockInfoUpdated != null)
            {
                handleStockInfoUpdated(this, e);
            }
        }

        protected virtual void OnStockPriceUpdated(StockPriceUpdateEventArgs e)
        {
            if (handleStockPriceUpdated != null)
            {
                handleStockPriceUpdated(this, e);
            }
        }

        protected virtual void OnUserJoined(UserEventArgs e)
        {
            if (UserJoined != null)
            {
                UserJoined(this, e);
            }
        }

        protected virtual void OnUserLeaves(UserEventArgs e)
        {
            if (UserLeaves != null)
            {
                UserLeaves(this, e);
            }
        }
        #endregion


        static DtcChannel()
        {
            User = new DtcUserObject(Environment.UserName, Environment.MachineName, DateTime.Now);
        }

        private DtcChannel()
        {

        }

        public static DtcChannel Instance
        {
            get
            {
                return instance;
            }
        }

        private bool ConnectToServer()
        {
            try
            {
                // Construct InstanceContext to handle messages on callback interface.                 
                Trace.WriteLine("Setting Context", TraceEnum.LoggedError);
                instanceContext = new InstanceContext(this);
                Trace.WriteLine("Set Context", TraceEnum.LoggedError);

                                
                Trace.WriteLine("Creating Factory", TraceEnum.LoggedError);
                factory = new DuplexChannelFactory<IDtcFeed>(instanceContext, "netTcpBinding_IDtcFeed");
                Trace.WriteLine("Created Factory", TraceEnum.LoggedError.ToString());

                Trace.WriteLine("Creating Channel", TraceEnum.LoggedError);
                channel = factory.CreateChannel();
                Trace.WriteLine("Created Channel", TraceEnum.LoggedError);

                                                    
            }
            catch (Exception e)
            {
                //do something
                Trace.WriteLine(e.ToString(), TraceEnum.LoggedError);
                return false;
            }

            return true;
        }

        public bool JoinFeed(int lastId)
        {
            ConnectToServer();

            try
            {
                Trace.WriteLine("Registering User", TraceEnum.LoggedError);
                channel.JoinFeed(User, lastId);
                Trace.WriteLine("Registered User", TraceEnum.LoggedError);
            }
            catch (CommunicationException c)
            {
                Trace.WriteLine(c, TraceEnum.LoggedError);
                return false;
            }
            return true;
        }
                

        #region IConduitCallback Members

                
        public void UserLeft(string member)
        {

        }

        #endregion

        public void CusipSelected(string cusip)
        {
            OnCusipSelected(new CusipSelectEventArgs(cusip));
        }


        #region IDtcFeedCallback Members

        public void FeedJoined(string feedName)
        {
            
        }

        public void PositionUpdated(RealTimePositionObject pos)
        {
            OnPositionUpdated(new PositionUpdateEventArgs(pos));
        }

        public void DtcMessageReceived(IDtcMessage msg)
        {
            OnDtcMessageReceived(new IncomingDtcMessageEventArgs(msg));
        }

        public void G1MessageReceived(G1RealTimeActivityObject msg)
        {
            OnG1MessageReceived(new IncomingG1MessageEventArgs(msg));
        }

        public bool SubscribedToType(SubscriptionType subType)
        {
            return User.Subscriptions.ContainsKey(subType);
        }

        public void Pong(string message)
        {

        }

        public void StockInfoUpdated(StockInfoObject si)
        {
            OnStockInfoUpdated(new StockInfoUpdateEventArgs(si));
        }

        public void StockPriceUpdated(StockPriceObject sp)
        {
            OnStockPriceUpdated(new StockPriceUpdateEventArgs(sp));
        }

        #endregion


        public void SubscribeToType(SubscriptionType subType)
        {
            if (!User.Subscriptions.ContainsKey(subType))
            {
                try
                {                    
                    channel.Subscribe(User, subType);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e, TraceEnum.LoggedError);
                }
                
            }
        }
    }
}
