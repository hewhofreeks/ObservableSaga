"use strict";
$(document).ready(($) => {

    var connection = new signalR.HubConnectionBuilder().withUrl("/counter").build();

    connection.on("Update", function (counterData) {
        $('#count').val(counterData.count);
    });

    connection.start().then(function () {
        connection.invoke('SubscribeToCounter', "new-id");
    });
});
