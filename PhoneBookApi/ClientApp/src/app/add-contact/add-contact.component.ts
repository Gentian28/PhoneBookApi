import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { PhoneBookService } from '../Services/phonebook.service'
import { PhoneBook } from '../Services/PhoneBookItem';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html'
})

export class AddContactComponent {
  public name: string;
  public type: number;
  public number: number;
  public newContact: FormGroup;

  constructor(
    private phonebookService: PhoneBookService,
    private formBuilder: FormBuilder,
    private location: Location
  ) {
    this.newContact = this.formBuilder.group({
      name: new FormControl(null, Validators.required),
      type: new FormControl(null, Validators.required),
      number: new FormControl(null, Validators.required)
    });
  }

  addContact(): void {
    this.phonebookService.addContact(this.newContact.value)
      .subscribe(() => this.goBack());
  }
  formControlName = new FormControl('', [
    Validators.required
  ]);

  goBack(): void {
    this.location.back();
  }

}
