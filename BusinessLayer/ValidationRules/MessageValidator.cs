using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        DbContext _dbContext;
        public MessageValidator()
        {
            //_dbContext = context;

            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Adresini Boş Geçemezsiniz");
            //RuleFor(x => x.ReceiverMail)
            //.NotEmpty().WithMessage("Alıcı Adresini Boş Geçemezsiniz")
            //.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
            //.MustAsync(ReceiverEmailExists).WithMessage("Bu e-posta sistemde kayıtlı değil.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Adını Boş Geçemezsiniz");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesajı Boş Geçemezsiniz");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen En Az 3 Karakter Girişi Yapınız");           
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen 100 Karakterden Fazla Değer Girişi Yapmayınız");
        }

    }
}
