import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PersonService, Person } from '../../../services/person-service';

@Component({
  selector: 'app-name-drop',
  standalone: true,
  imports: [ FormsModule, CommonModule ],
  providers: [PersonService],
  templateUrl: './name-drop.html',
  styleUrls: ['./name-drop.scss'] 
})
export class NameDropComponent {
  firstName = '';
  message = '';
  person?: Person;

  constructor(private personService: PersonService) {}

  showSurprise() {
    if (!this.firstName.trim()) {
      this.message = "Oops 😅 Please enter your name first!";
      return;
    }

    this.personService.getPerson(this.firstName).subscribe({
      next: (data) => {
        this.person = data;
        this.message = `Hello: ${data.name}, you’re truly one of a kind! 🚀. I feel like you're from ${data.nationality} and you're a ${data.gender}, with an age of ${data.age} ✅.`;
      },
      error: (err) => {
        this.message = "⚠️ Error calling API: " + err.message;
      }
    });
  }
}
