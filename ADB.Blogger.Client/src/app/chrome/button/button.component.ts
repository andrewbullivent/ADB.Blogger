import { Component,   EventEmitter,   Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent {

  @Input() url: string |undefined ;
  @Input() type: string | undefined;
  @Input() buttonText: string = "button";

  
  @Output() public onClick = new EventEmitter();

  private primaryButtonColours ="bg-green-500 hover:from-green-500 hover:to-green-400 text-white hover:ring-green-400" 
  private secondaryButtonColours =" bg-slate-500 hover:from-slate-500 hover:to-slate-400 text-white hover:ring-slate-400"
  private warningButtonColours = "bg-red-500 hover:from-red-500 hover:to-red-400 text-white hover:ring-red-400"


  public buttonColour: string = this.primaryButtonColours;
  constructor()
  {

  }

  doClick(){
    if(!this.url){
      this.onClick.emit();
    }
  }

  ngOnInit() {

    if (this.type == undefined) {
      this.type = "primary";
    }


    switch (this.type) {
      case "secondary":
        this.buttonColour = this.secondaryButtonColours;
        break;
      case "warning":
        this.buttonColour = this.warningButtonColours;
        break;
      case "primary":  
      default:
        this.buttonColour = this.primaryButtonColours;
        break;
    }
    
  }
}
