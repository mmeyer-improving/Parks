var parksList = document.querySelector('#parksList');
var searchBox = document.querySelector('#searchBox');
var parksPromise;

function makeParksList(data) {
    data.forEach(park => {
        console.log("makeParksList Ran")
        var search = searchBox.textContent;
        if (search === null) {
            createAndAppendElement(park)
        } else if (park.description.includes(search) || park.ParkName.includes(search) ) {
            createAndAppendElement(park)
        }
    });
}

function createAndAppendElement(park) {
    //Create Header
    var header = document.createElement('H1');
    header.classList.toggle('header');
    header.textContent = park.parkName;
    parksList.appendChild(header);
    //Borough
    var borough = document.createElement('P');
    borough.innerHTML = `<em>Borough</em>: ${park.borough}`;
    parksList.appendChild(borough);
    //Acres
    var acres = document.createElement('P');
    acres.innerHTML = `<em>Acres</em>: ${park.acres}`;
    parksList.appendChild(acres);
    //Description
    var description = document.createElement('P');
    description.innerHTML = `<em>Description</em><br><br>${park.description}`;
    parksList.appendChild(description);
    //Horizontal rule
    var hr = document.createElement('HR');
    parksList.appendChild(hr)
}

function getParks() {
    parksList.textContent = ''
    fetch('parksAJAX')
        .then(response => response.json())
        .then((data) => {
            console.log(data)
            makeParksList(data)
            //searchBox.addEventListener('keyup', makeParksList(data))
        });
}
    
parksList.textContent = "";
getParks();

searchBox.addEventListener('keyup', function (e) {
    if (e.code === 'Enter') {
        e.preventDefault();
        console.log("search function ran")
        getParks()
    }
});