import {Component, Prop, h, State, Listen} from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-main',
  styleUrl: 'apx-main.scss',
  shadow: true,
})
export class ApexMain {
  @Prop() ctx: Context;
  @Prop({mutable: true, reflect: true}) process: string;
  @Prop({mutable: true, reflect: true}) activity: string;
  @Prop({mutable: true, reflect: true}) sessionId: string;

  @State() baseUrl: string;

  connectedCallback() {
    this.baseUrl = this.ctx.config.getSetting("[baseUrl]");
  }

  @Listen('hashchange', { capture: true, target: 'window' })
  changeProcess() {
    const params = window.location.hash.replace('#', '').split('-');

    if(this.process === params[0] || params[0] === "default")
      return;

    this.process = params[0];
    this.activity = params.length > 1 ? params[1] : 'start';
    this.sessionId = params.length > 2 ? params[2] : null;

    window.location.hash = this.process;
  }

  render() {
    return <polaris-workflow
        parent={this.ctx}
        process={this.process}
        activity={this.activity}
        sessionId={this.sessionId}>
    </polaris-workflow>
    ;
  }
}
