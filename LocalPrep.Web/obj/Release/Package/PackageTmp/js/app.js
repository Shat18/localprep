'use strict';

const express = require('express');
var http = require('http');
const app = express();
var localtunnel = require('localtunnel');
var clear = require('clear');
var chalk = require('chalk');
var request = require('request');
var curl = require('curl');
var fs = require('fs');
var popoUrl;
var data_body;
var body_man;
var cookieParser = require('cookie-parser');
var packageJSON = fs.readFileSync("./package.json");
packageJSON = packageJSON.toString();
packageJSON = JSON.parse(packageJSON);
var placeholder = '-webserver'
var name = packageJSON.name;
var webhost = name + placeholder;
require('dotenv').config();
const Logger = require('logdna');
const cors = require('cors');
const bodyParser = require('body-parser');
const options = {
  hostname: webhost,
  ip: '',
  mac: '',
  app: packageJSON.name,
};
const log = Logger.setupDefaultLogger(process.env.key, options);
console.log = function(string){
  log.log(string);
  console.debug(string);
}


var tunnelNow;



app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cors());
app.use(cookieParser())

app.get('/', (req, res) => {
  
    var data = '<h1><marquee direction=right>This is 2k Website</marquee></h1>';
    var nowUrl = fs.readFileSync("./now.json");
      nowUrl = nowUrl.toString();
      nowUrl = JSON.parse(nowUrl);
 
      nowUrl = 'https://' + nowUrl.alias;
      setTimeout(function(){
        //console.log(' ');
        //console.log(chalk.bgBlue.black(nowUrl + ' is reachable from internet!'));
      }, 4600);
     
     res.send(data);
     
          })

     
        
    


app.get('/cookies', function (req, res) {
  // Cookies that have not been signed
  console.log('Cookies: ',req.cookies);

  // Cookies that have been signed
  console.log('Signed Cookies: ', req.signedCookies)
  res.send(req.cookies);
})

app.post('/api/log', (req, res) => {
  log.info(`Application Name: ${req.body.app_name}`);
  log.info(`Application Description: ${req.body.app_description}`);
  console.log("Log something now");
  res.json({ message: "Logged something successfully" });
});


var port = process.env.port || 6450;

app.listen(port, () => {

});
clear();

  tunnel();
  function tunnel() {
      var tunnel = localtunnel(port, function(err, tunnel) {
      if (err) {
          console.log('reloading tunnel...');
          tunnel();
      }
      else {
          console.log(chalk.bgBlue.black(' Server started at port: ') + chalk.bgWhite.black(' ' + port + ' \n'));
         
        
          //console.log(chalk.bgWhite.black(' Online: '));
          console.log(chalk.bgWhite.black(' ' + tunnel.url + ' '));
          var nowUrl = fs.readFileSync("./now.json");
            nowUrl = nowUrl.toString();
            nowUrl = JSON.parse(nowUrl);
            nowUrl = nowUrl.alias; 
            

            tunnelNow = tunnel.url;
            
              
        
            setTimeout(function(){
              var j = request.jar();
              //console.log(chalk.yellow(tunnel.url))
              var url = 'https://' + nowUrl + '/redirect?url=' + tunnel.url;
              //console.log(chalk.bgRed.black(url));
              var cookie = request.cookie('__auth2k=tanktheory:017889allo');
              j.setCookie(cookie, url);
              request(
                { method: 'GET',
                uri: url,
                jar: j,
                gzip: true
                }
              , function (error, response, body) {
                  // body is the decompressed response body
                  //console.log('server encoded the data as: ' + (response.headers['content-encoding'] || 'identity'))
                  //console.log('the decoded data is: ' );
                  //console.log(response);
                  data_body = response;
                  
                 
                  body_man = JSON.parse(body);
                  body_man = Object.entries(body_man)
                  for(var i=0; i < (body_man.length); i++){
                    var man = body_man[i].toString().replace(",", ":  ");
                    man = man.split(":  ");
                    console.log(chalk.bgWhite.black(' ' + man[0] + '\:') + chalk.bgBlue.black.inverse(' ' + man[1] + ' '));
                  }
                  
            
                }
              )
              .on('data', function(data) {
                // decompressed data as it is received
                //console.log('decoded chunk: ' + data)
                //console.log(data.toString())
              })
              .on('response', function(response) {
                // unmodified http.IncomingMessage object
                response.on('data', function(data) {
                  
                  // compressed data as it is received
                  //console.log('received ' + data.length + ' bytes of compressed data')
                })})
              //console.log(' ');
              //console.log(chalk.bgBlue.black('rotating forwarding addresses for domain: ' + nowUrl + '..'));
              //console.log(' ');
              
            }, 3000);

            setTimeout(function(){
              console.info(' ')
              if (data_body){
              var man = data_body.body;
              man = JSON.parse(man);
              }
              if (man.status === 200) {
              console.log(chalk.bgBlue.black('Rotation Completed!'));
              console.log(chalk.bgYellow.black(nowUrl + ' --> ' + man.target_url + ' [ ' + man.req_from + ' <== ' + man.origin + ' ] '));
              } else {
                console.log(chalk.bgRed.black('Rotation Completed..'));
                console.log(chalk.bgYellow.black(' ' + nowUrl +  ' --> ') + chalk.bgRed.black(' FAILED '));
              }
            }, 4300);
          
          

      }
    });

  tunnel.on('close', function() {
      // tunnels are closed
  });
  }


