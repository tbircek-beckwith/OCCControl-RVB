using System;
using System.Threading.Tasks;
using Automatak.DNP3.Interface;

namespace rvb_sim.dnp
{
    public class AnalogOutputControl
    {

        public ushort Index { get; }
        public decimal Value { get; }
        public AOType Type { get; }

        public enum AOType
        {
            Integer16,
            Integer32,
            Float32,
            Double64
        };

        public AnalogOutputControl() { }

        public AnalogOutputControl(ushort index, decimal value, AOType type)
        {
            Index = index;
            Value = value;
            Type = type;
        }

        //public void DirectOperateAction()
        //{

        //}

        public Func<ICommandProcessor, Task<CommandTaskResult>> DirectOperateAction
        {
            get
            {
                var index = Index;
                var value = Value;
                var type = Type;

                switch (type)
                {
                    case (AOType.Integer16):
                        return (ICommandProcessor cp) => cp.DirectOperate(new AnalogOutputInt16(SafeConvert.ToInt16(value)), index, TaskConfig.Default);
                    case (AOType.Integer32):
                        return (ICommandProcessor cp) => cp.DirectOperate(new AnalogOutputInt32(SafeConvert.ToInt32(value)), index, TaskConfig.Default);
                    case (AOType.Float32):
                        return (ICommandProcessor cp) => cp.DirectOperate(new AnalogOutputFloat32(SafeConvert.ToSingle(value)), index, TaskConfig.Default);
                    default:
                        return (ICommandProcessor cp) => cp.DirectOperate(new AnalogOutputDouble64(SafeConvert.ToDouble(value)), index, TaskConfig.Default);
                }
            }
        }

        public Func<ICommandProcessor, Task<CommandTaskResult>> SelectAndOperateAction
        {
            get
            {

                var index = Index;
                var value = Value;
                var type = Type;

                switch (Type)
                {
                    case (AOType.Integer16):
                        return (ICommandProcessor cp) => cp.SelectAndOperate(new AnalogOutputInt16(Convert.ToInt16(Value)), index, TaskConfig.Default);
                    case (AOType.Integer32):
                        return (ICommandProcessor cp) => cp.SelectAndOperate(new AnalogOutputInt32(Convert.ToInt32(value)), index, TaskConfig.Default);
                    case (AOType.Float32):
                        return (ICommandProcessor cp) => cp.SelectAndOperate(new AnalogOutputFloat32(Convert.ToSingle(value)), index, TaskConfig.Default);
                    default:
                        return (ICommandProcessor cp) => cp.SelectAndOperate(new AnalogOutputDouble64(Convert.ToDouble(value)), index, TaskConfig.Default);
                }
            }
        }
    }
}
