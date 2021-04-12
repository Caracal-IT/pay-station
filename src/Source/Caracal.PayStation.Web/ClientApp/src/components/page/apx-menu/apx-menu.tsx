import {Component, Listen, Prop, State, h} from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-menu',
  styleUrl: 'apx-menu.scss',
  shadow: true,
})
export class ApexMenu {
  @Prop() ctx: Context;
  @Prop({attribute: "items"}) items;

  @State() menuItems = [];
  @State() process: string;

  async connectedCallback() {
    this.setActiveMenuItem();

    if(this.items) {
      const items = this.ctx.model.getValue(this.items);
      if (items)
        this.menuItems = [...items];
    }
  }

  @Listen('hashchange',{ capture: true, target: 'window' })
  setActiveMenuItem() {
    const params = window.location
                         .hash
                         .replace('#', '')
                         .split('-');

    if(!params[0])
      window.location.hash = 'home';

    if(this.process && this.process === params[0]) return;

    this.process = params[0];
  }

  @Listen('wfMessage', { capture: true, target: 'document' })
  wfMessage(event) {
    if(this.shouldChangeLocation(event))
      window.location.hash = event.detail.process;
  }

  shouldChangeLocation(event) {
    return event.detail.type
      && event.detail.type === 'PROCESS_CHANGED'
      && event.detail.process !== "default"
      && event.detail.metadata
      && event.detail.metadata.stack
      && event.detail.metadata.stack.length === 0;
  }

  render() {
    return <nav>
      {
        this.menuItems.map(item => <a href={'#' + item.process} class={this.process === item.process?'active':''}>{item.name}</a>)
      }
    </nav>;
  }
}
