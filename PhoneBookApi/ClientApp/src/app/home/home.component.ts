import { Component, OnInit } from '@angular/core';
import { PhoneBookService } from '../Services/phonebook.service';

import { PhoneBook } from '../Services/PhoneBookItem';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent implements OnInit {
  phonebook: PhoneBook[];
  constructor(private phonebookService: PhoneBookService) { }

  ngOnInit() {
    this.getPhoneBook();
  }

  getPhoneBook(): void {
    this.phonebookService.getPhoneBookItems()
      .subscribe(phonebook => this.phonebook = phonebook);
  }

  delete(phonebookItem: PhoneBook, id: number): void {
    this.phonebook = this.phonebook.filter(p => p !== phonebookItem);
    this.phonebookService.deleteContact(id)
      .subscribe();
  }
}
