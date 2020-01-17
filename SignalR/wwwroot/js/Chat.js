
var myConnectionId = "";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReciveMessage", function (message, ConnectionId) {
    $('[name="Message"]').val('');
    if (myConnectionId === ConnectionId) {
        $('#MyMessage').append('<li class="list-group-item">' + message + '</li>')
    }
    else {
        $('#ReciveMessage').append('<li class="list-group-item">' + message + '</li>')
    }
})

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("SetConnection", function (connectionId) {
    myConnectionId = connectionId;
    console.log(myConnectionId);
})

$('#send').on('click', function (e) {

    var message = $('[name="Message"]').val();

    connection.invoke("SendMessageToAll", message).catch(function (err) {
        return console.error(err.toString());
    });
    e.preventDefault();
})