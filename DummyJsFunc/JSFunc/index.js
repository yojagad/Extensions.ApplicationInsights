module.exports = async function (context, req, appInsightsTag) {
    context.log('JavaScript HTTP trigger function processed a request.');
    var a = context.blah;
    var activityId = appInsightsTag.Id; // This will be used for tracing
    context.bindings.appInsightsTag.Tags.push({'key':'blah', 'value': 'blahblah'}); // This will flow back through the output binding and AddAsync will take care of using this
};