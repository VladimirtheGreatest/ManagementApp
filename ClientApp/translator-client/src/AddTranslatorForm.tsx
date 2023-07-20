import React, { useState } from 'react';

interface CreateTranslatorRequestDto {
    name: string;
    hourlyRate: string;
    creditCardNumber: string;
    status: number
}

interface Props {
    onSubmit: (formData: CreateTranslatorRequestDto) => void;
}

const AddTranslatorForm = ({ onSubmit }:Props) => {
    const [formData, setFormData] = useState<CreateTranslatorRequestDto>({
        name: '',
        hourlyRate: '',
        creditCardNumber: '',
        status: 1 //this should be prepopulated from available statuses from db
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const { name, hourlyRate, creditCardNumber } = formData;
        if (name && hourlyRate && creditCardNumber) {
            onSubmit(formData);
        }
    };

    return (
        <form className="add-translator-form" onSubmit={handleSubmit}>
            <div>
                <label htmlFor="name">Name:</label>
                <input
                    type="text"
                    id="name"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                />
            </div>

            <div>
                <label htmlFor="hourlyRate">Hourly Rate:</label>
                <input
                    type="text"
                    id="hourlyRate"
                    name="hourlyRate"
                    value={formData.hourlyRate}
                    onChange={handleChange}
                    required
                />
            </div>

            <div>
                <label htmlFor="creditCardNumber">Credit Card Number:</label>
                <input
                    type="text"
                    id="creditCardNumber"
                    name="creditCardNumber"
                    value={formData.creditCardNumber}
                    onChange={handleChange}
                    required
                />
            </div>

            <button type="submit">Add Translator</button>
        </form>
    );
};

export default AddTranslatorForm;


