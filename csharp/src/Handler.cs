using NecklaceRefactoringKata.Enums;
using NecklaceRefactoringKata.JewelleryTypes;

namespace NecklaceRefactoringKata
{
    public abstract class Handler
    {
        protected readonly Handler? next;

        public Handler() { }
        public Handler(Handler next)
        {
            this.next = next;
        }

        public virtual void Handle(JewelleryBase item, JewelleryStorage storage)
        {
            next?.Handle(item, storage);
        }
        
        public static Handler CreatePackNecklaceHandler()
        {
            return new DiamondHander(
                new HeavyItemHander(
                    new PendantNecklaceHandler(
                        new TreeHandler())));
        }
    }

    public class DiamondHander : Handler
    {
        public DiamondHander(Handler next) : base(next) { }

        public override void Handle(JewelleryBase item, JewelleryStorage storage)
        {
            if (item.Stone == Jewel.Diamond)
            {
                storage.Safe.Add(item);
                return;
            }

            base.Handle(item, storage);
        }
    }

    public class HeavyItemHander : Handler
    {
        public HeavyItemHander(Handler next) : base(next) { }

        public override void Handle(JewelleryBase item, JewelleryStorage storage)
        {
            if (!item.IsHeavy())
            {
                storage.Box.TopShelf.Add(item);
                return;
            }

            base.Handle(item, storage);
        }
    }

    public class PendantNecklaceHandler : Handler
    {
        public PendantNecklaceHandler(Handler next) : base(next) { }

        public override void Handle(JewelleryBase item, JewelleryStorage storage)
        {
            if (item is PendantNecklace pendantNecklace)
            {
                storage.Tree.Add(pendantNecklace.Chain);
                storage.Box.TopShelf.Add(pendantNecklace.Pendant);
                return;
            }

            base.Handle(item, storage);
        }
    }

    public class TreeHandler : Handler
    {
        public override void Handle(JewelleryBase item, JewelleryStorage storage)
        {
            storage.Tree.Add(item);
        }
    }
}