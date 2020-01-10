import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject, Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView
} from 'angular-calendar';
import { UserSpecificFormsService } from 'src/app/form-module/services/userspecificform.service';
import { FormDataModel } from 'src/app/form-module/models/form-data-model';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  green: {
    primary: '#32CD32'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  actions: CalendarEventAction[] = [
    // {
    //   label: '<i class="fa fa-fw fa-pencil"></i>',
    //   a11yLabel: 'Click',
    //   onClick: ({ event }: { event: CalendarEvent }): void => {
    //     this.handleEvent('clicked', event);
    //   }
    // },
    // {
    //   label: '<i class="fa fa-fw fa-times"></i>',
    //   a11yLabel: 'Delete',
    //   onClick: ({ event }: { event: CalendarEvent }): void => {
    //     this.events = this.events.filter(iEvent => iEvent !== event);
    //     this.handleEvent('Deleted', event);
    //   }
    // }
  ];

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [];
  event: CalendarEvent;
  data: Array<FormDataModel>;

  activeDayIsOpen: boolean = false;

  constructor(private modal: NgbModal, private userSpecificFormService: UserSpecificFormsService) { }

  // tslint:disable-next-line: use-lifecycle-interface
  ngOnInit() {
    this.userSpecificFormService.getForms('all')
      .subscribe((res) => {
        this.data = res;
        this.data.forEach(element => {
          if (element.FormType == 'Release') {
            this.event = {
              id: element.FormId,
              title: element.FormName,
              allDay: true,
              start: new Date(element.EffectiveDate.StartDate),
              end: new Date(element.EffectiveDate.EndDate),
              color: colors.green

            };
          }
          if (element.FormType == 'Leave') {

            this.event = {
              id: element.FormId,
              title: element.FormName,
              allDay: true,
              start: new Date(element.EffectiveDate.StartDate),
              end: new Date(element.EffectiveDate.EndDate),
              color: colors.red
            };
          }
          this.events.push(this.event);
        });
        this.refresh.next(this.events);
      });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map(iEvent => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  // addEvent(): void {
  //   this.events = [
  //     ...this.events,
  //     {
  //       title: 'New event',
  //       start: startOfDay(new Date()),
  //       end: endOfDay(new Date()),
  //       color: colors.red,
  //       draggable: true,
  //       resizable: {
  //         beforeStart: true,
  //         afterEnd: true
  //       }
  //     }
  //   ];
  // }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter(event => event !== eventToDelete);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
