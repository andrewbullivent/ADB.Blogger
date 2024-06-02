import { Component } from '@angular/core';
import Config  from '../assets/config/app.config.json'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  {


  title = Config.appSettings.appTitle;
  

  
}
