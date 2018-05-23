export const BookStatusKey = {
    ToReading: 'To Reading',
    Reading: 'Reading',
    StartedButPostponed: 'Started But Postponed',
    StartedButВiscarded: 'Started But Вiscarded',
    Read: 'Read'
};

export const BookStatus = {
    [BookStatusKey.ToReading]: 'ToReading',
    [BookStatusKey.Reading]: 'Reading',
    [BookStatusKey.StartedButPostponed]: 'StartedButPostponed',
    [BookStatusKey.StartedButВiscarded]: 'StartedButВiscarded',
    [BookStatusKey.Read]: 'Read'
};

export interface SelectListItem {
    text: string;
    value: string;
}

export function generateStatusSelectItems (): SelectListItem[] {
    const options: SelectListItem[] = [];
    for(let key in BookStatus) {
        if(key) {
            const value = BookStatus[key];
            const text = key;
            options.push({value, text});
        }
    }
    return options;
}