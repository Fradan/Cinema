using Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWeb.Validation
{
    public class SessionValidator : AbstractValidator<SessionViewModel>
    {
        public SessionValidator()
        {
            RuleFor(session => session.CinemaId).NotEmpty();
            RuleFor(session => session.FilmId).NotEmpty();
            RuleFor(session => session.SessionTime).NotEmpty();
        }

    }
}
