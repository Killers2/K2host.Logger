/*
' /====================================================\
'| Developed Tony N. Hyde (www.k2host.co.uk)            |
'| Projected Started: 2019-11-01                        | 
'| Use: General                                         |
' \====================================================/
*/

using K2host.Logger.Interfaces;

namespace K2host.Logger.Delegates
{

    public delegate void OnLogCreatedEvent(ILogEntrie[] e);

    public delegate void OnRetryAddEvent(ILogEntrie e);

    public delegate void OnRetryEvent();

}