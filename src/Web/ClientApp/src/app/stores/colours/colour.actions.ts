export namespace ColourActions {
  export class LoadColours {
    static readonly type = '[Colour] Load Colours';
  }

  export class SetLoading {
    static readonly type = '[Colour] Set Loading';
    constructor(public isLoading: boolean) { }
  }
}
