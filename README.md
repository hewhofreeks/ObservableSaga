# ObservableSaga

First run at an observale saga state using NServiceBus + SignalR

Should be easy enough to run the sample. Pull it up in a few different browsers and you can see how when you interact with one counter the rest of them change. 


I'm utilizing SignalR to update the clients and NServiceBus Sagas to maintain state.

Currently CounterSaga calls the HubContext which updates the client with the updated saga data. 




To dos:

-Automate update publishes to fire on every saga data property changes, instead of having to call it manually every time the data changes

-Create front-end library to be loaded via npm to automate subscribe/listener calls

-Figure out best way to expire this sort of saga

