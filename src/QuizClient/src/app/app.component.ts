import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth/auth.service';

/**
 * The application component.
 */
@Component({
  selector: 'quiz-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Quiz';

  constructor(private authService: AuthService) {

  }

  ngOnInit() {
    this.authService.updateStatus();
  }
}
