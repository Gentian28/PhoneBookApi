import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { PhoneBookService } from '../Services/phonebook.service'
import { PhoneBook } from '../Services/PhoneBookItem';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
})
export class ContactComponent implements OnInit {
  @Input() contact: PhoneBook;

  updateContact: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private phonebookService: PhoneBookService,
    private location: Location
  ) {
    this.updateContact = new FormGroup({
      name: new FormControl(null, Validators.required),
      type: new FormControl(null, Validators.required),
      number: new FormControl(null, Validators.required)
    });
  }

  ngOnInit(): void {
    this.getPhoneBookItem();
  }

  getPhoneBookItem(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.phonebookService.getPhoneBookItem(id)
      .subscribe(contact => this.contact = contact);
  }

  formControlName = new FormControl('', [
    Validators.required
  ]);

  goBack(): void {
    this.location.back();
  }

  save(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.phonebookService.updataContact(id, this.contact)
      .subscribe(() => this.goBack());
  }
}
