import React from 'react';
import AddTranslatorForm from './AddTranslatorForm';
import axios from 'axios';

interface CreateTranslatorRequestDto {
    name: string;
    hourlyRate: string;
    creditCardNumber: string;
}

const ParentComponent = () => {
    const handleFormSubmit = async (formData: CreateTranslatorRequestDto) => {
        try {
            // Make the API call using axios
            const response = await axios.post('/api/translators', formData);

            // Handle the response (if needed)
            console.log('Response:', response.data);
        } catch (error) {
            // Handle errors (if any)
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <h1>Add Translator</h1>
            <AddTranslatorForm onSubmit={handleFormSubmit} />
        </div>
    );
};

export default ParentComponent;
