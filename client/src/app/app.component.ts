import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'DatingApp';
  Users: any;
  constructor(private Http:HttpClient) {

  }
    ngOnInit(): void {
      this.Http.get('https://localhost:7118/api/users').subscribe({
        next: Response => this.Users = Response,
        error: Error => console.log(Error),
        complete:()=>console.log('Request has completed')
      });
    }
}
