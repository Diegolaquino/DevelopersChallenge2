import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'clientapp';
  constructor(private http: HttpClient){}

  inputFileChange(event){
    if(event.target.files && event.target.files[0]){
      const ofxs = event.target.files;

      const formData = new FormData();

      for (let i = 0; i < ofxs.length; i++) {
        formData.append('ofx'+ (i + 1), ofxs[i]);
      }
      
      this.http.post('http://localhost:61056/api/SampleData/PostFile', formData).subscribe(response => console.log(response));
      alert('Enviado');
    }
  }
}
