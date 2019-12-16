"use strict";
$(document).ready(($) => {

    var connection = new signalR.HubConnectionBuilder().withUrl("/counter").build();

    connection.on("Update", function (counterData) {

        //Will need to separate update listeners based on observable IDs, otherwise "Update" will be called for EACH subscription
        $(`[data-counter-id='${counterData.counterID}'] .count`).val(counterData.count);
    });

    connection.start().then(async function () {
        await connection.invoke('SubscribeToCounter', "counter-1");
        await connection.invoke('SubscribeToCounter', "counter-2");
    });
});
