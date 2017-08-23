import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { MasjidService } from '../services/masjid.service'
import { Masjid } from '../models/Masjid';

@Component({
  selector: 'app-root',
  providers: [MasjidService],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(
    private _httpService: Http,
    private MService : MasjidService
  ) { }

  private masjids: Masjid[] = [];

  ngOnInit() {
    this.MService.getMasjids().subscribe(
      masjids => {
        console.log(masjids);
        this.masjids = masjids;
      }
    )
  }
}
