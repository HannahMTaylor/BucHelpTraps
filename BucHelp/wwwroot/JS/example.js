//namespace BucHelp.wwwroot.JS
//{
//    public class example
//    {
//    }
//}
function JSAlert() {
    alert("Hello");
}

async function filter() {

    const artsandsciencesCheckbox = document.querySelector('#artsandsciences');
    const businessandtechnologyCheckBox = document.querySelector('#businessandtechnology');
    const clinicalandrehabCheckbox = document.querySelector('#clinicalandrehabilitativehealthsciences');
    const nursingCheckbox = document.querySelector('#nursing');
    const publichealthCheckBox = document.querySelector('#publichealth');
    const honorsCheckbox = document.querySelector('#honors');
    const graduateCheckbox = document.querySelector('#graduate');
    const dormCheckBox = document.querySelector('#dorm');
    const campusCheckbox = document.querySelector('#campus');

    let returnValues = [10];

    if (artsandsciencesCheckbox.checked)
        returnValues[0] = "artsandsciences";
    else
        returnValues[0] = "";
    if (businessandtechnologyCheckBox.checked)
        returnValues[1] = "businessandtechnology";
    else
        returnValues[1] = "";
    if (clinicalandrehabCheckbox.checked)
        returnValues[2] = "clinicalandrehabilitativehealthsciences";
    else
        returnValues[2] = "";
    if (nursingCheckbox.checked)
        returnValues[3] = "nursing";
    else
        returnValues[3] = "";
    if (publichealthCheckBox.checked)
        returnValues[4] = "publichealth";
    else
        returnValues[4] = "";
    if (honorsCheckbox.checked)
        returnValues[5] = "honors";
    else
        returnValues[5] = "";
    if (graduateCheckbox.checked)
        returnValues[6] = "graduate";
    else
        returnValues[6] = "";
    if (dormCheckBox.checked)
        returnValues[7] = "dorm";
    else
        returnValues[7] = "";
    if (campusCheckbox.checked)
        returnValues[8] = "campus";
    else
        returnValues[8] = "";
    //alert(returnValues);

    return returnValues;
}